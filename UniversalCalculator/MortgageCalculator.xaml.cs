using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			int principal = int.Parse(principalborrowTextBox.Text);
			int years = int.Parse(yearsTextBox.Text);
			int months = int.Parse(andMonthsTextBox.Text);
			double yearlyInterestRate = double.Parse(yearlyInterestRateTextBox.Text);

			double monthlyInterestRate = yearlyInterestRate / 12;


			int totalMonths = (years * 12) + months;

			double monthlyRepayment;


			double numerator = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths);
			double denominator = Math.Pow(1 + monthlyInterestRate, totalMonths) - 1;
			monthlyRepayment = principal * (numerator / denominator);





			monthlyInterestRateTextBox.Text = monthlyInterestRate.ToString("");
			monthlyRepaymentTextBox.Text = monthlyRepayment.ToString("C");


		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
