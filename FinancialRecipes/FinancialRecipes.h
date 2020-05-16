// FinancialRecipes.h

#pragma once
using namespace System::Runtime::InteropServices;

#include "FR/fin_recipes.h"

using namespace System;

namespace FinancialRecipes 
{
	public ref class FR
	{
	public:
		static double OptionPriceImpliedVolatilityCallBlackScholes (double S, double K, double r, int days_left, double option_price)
		{
			double iv = option_price_implied_volatility_call_black_scholes_newton (S, K, r, days_left / 365.0, option_price);
			if (iv <= 0.0)
			{
				iv = option_price_implied_volatility_call_black_scholes_bisections (S, K, r, days_left / 365.0, option_price);
				if (iv < 0.0)
				{
					throw gcnew Exception (String::Format ("Failed to compute call iv for stockprice {0:N3}, strike {1:N3}, daysleft {2}, option price {3:N3}", S, K, days_left, option_price));
				}
			}
			return iv;
		}

		static double OptionPriceImpliedVolatilityPutBlackScholes (double S, double K, double r, int days_left, double option_price)
		{
			double iv = option_price_implied_volatility_put_black_scholes_newton (S, K, r, days_left / 365.0, option_price);
			if (iv <= 0.0)
			{
				iv = option_price_implied_volatility_put_black_scholes_bisections (S, K, r, days_left / 365.0, option_price);
				if (iv <= 0.0)
				{
					throw gcnew Exception (String::Format ("Failed to compute put iv for stockprice {0:N3}, strike {1:N3}, daysleft {2}, option price {3:N3}", S, K, days_left, option_price));
				}
			}
			return iv;
		}

		static void OptionPricePartialsCallBlackScholes (double S, double K, double r, double sigma, int days_left, [Out] double% delta, [Out] double% gamma, [Out] double% theta, [Out] double% vega, [Out] double% rho)
		{
			double Delta, Gamma, Theta, Vega, Rho;

			option_price_partials_call_black_scholes (S, K, r, sigma, days_left / 365.0, Delta, Gamma, Theta, Vega, Rho);
			delta = Delta;
			gamma = Gamma;
			theta = Theta / 365.0;
			vega = Vega / 100.0;
			rho = Rho / 100.0;
		}

		static void OptionPricePartialsPutBlackScholes (double S, double K, double r, double sigma, int days_left, [Out] double% delta, [Out] double% gamma, [Out] double% theta, [Out] double% vega, [Out] double% rho)
		{
			double Delta, Gamma, Theta, Vega, Rho;

			option_price_partials_put_black_scholes (S, K, r, sigma, days_left / 365.0, Delta, Gamma, Theta, Vega, Rho);
			delta = Delta;
			gamma = Gamma;
			theta = Theta / 365.0;
			vega = Vega / 100.0;
			rho = Rho / 100.0;
		}
	};
}
