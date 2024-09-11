using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyCalc : Page
	{
		// Instantiate exchange rate values
		private readonly Dictionary<string, decimal> FROM_USD = new Dictionary<string, decimal>
			{
				{ "EUR", 0.85189982m },
				{ "GBP", 0.72872436m },
				{ "INR", 74.257327m }
			};
		private readonly Dictionary<string, decimal> FROM_EUR = new Dictionary<string, decimal>
			{
				{ "USD", 1.1739732m },
				{ "GBP", 0.8556672m },
				{ "INR", 87.00755m }
			};
		private readonly Dictionary<string, decimal> FROM_GBP = new Dictionary<string, decimal>
			{
				{ "USD", 1.371907m },
				{ "EUR", 1.1686692m },
				{ "INR", 101.68635m }
			};
		private readonly Dictionary<string, decimal> FROM_INR = new Dictionary<string, decimal>
			{
				{ "USD", 0.011492628m },
				{ "EUR", 0.013492774m },
				{ "GBP", 0.0098339397m }
			};

		public CurrencyCalc()
		{
			this.InitializeComponent();
		}

		private async void convertButton_Click(object sender, RoutedEventArgs e)
		{
			decimal inputAmount;
			try
			{
				inputAmount = decimal.Parse(amountTextBox.Text);
			}
			catch
			{
				var dialogMessage = new MessageDialog("Please enter a number for the currency to convert.");
				await dialogMessage.ShowAsync();
				return;
			}

			// Extract currency codes and names from the selections.
			string fromCurrencyName = fromComboBox.SelectedItem.ToString().Substring(6);
			string fromCurrencyCode = fromComboBox.SelectedItem.ToString().Substring(0,3);
			string toCurrencyName = toComboBox.SelectedItem.ToString().Substring(6);
			string toCurrencyCode = toComboBox.SelectedItem.ToString().Substring(0,3);

			if (fromCurrencyCode == toCurrencyCode)
			{
				var dialogMessage = new MessageDialog("Cannot convert to the same currency. Please select different currencies.");
				await dialogMessage.ShowAsync();
				return;
			}

			decimal forwardExchangeRate = GetExchangeRate(fromCurrencyCode, toCurrencyCode);
			decimal reverseExchangeRate = GetExchangeRate(toCurrencyCode, fromCurrencyCode);
			decimal result = inputAmount * forwardExchangeRate;

			// Output results
			conversionDetailsTextBlock.Text = $"{inputAmount.ToString()} {fromCurrencyName}s =";
			outputTextBlock.Text = $"${result} {toCurrencyName}s";
			exchangeRateTextBlock.Text = $"1 {fromCurrencyCode} = {forwardExchangeRate} {toCurrencyName}s";
			reverseExchangeRateTextBlock.Text = $"1 {toCurrencyName} = {reverseExchangeRate} {fromCurrencyCode}";
		}

		/// <summary>
		/// Returns the exchange rate, given a 'from' and a 'to' currency.
		/// </summary>
		/// <param name="baseCode"></param>
		/// <param name="targetCode"></param>
		/// <returns></returns>
		decimal GetExchangeRate(string baseCode, string targetCode)
		{
			Dictionary<string, decimal> currencyDict;

			switch (baseCode)
			{
				case "USD":
					currencyDict = FROM_USD;
					break;
				case "EUR":
					currencyDict = FROM_EUR;
					break;
				case "GBP":
					currencyDict = FROM_GBP;
					break;
				case "INR":
					currencyDict = FROM_INR;
					break;
				default:
					currencyDict = FROM_USD;
					break;
			}

			return currencyDict[targetCode];
		}

		// Return to the main menu
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
