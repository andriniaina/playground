

# Bases
## Standard deviation \sigma = \sqrt(E[(X-µ)^2]) = \sqrt(E[X^2]-E[X]^2)
	Variance = \sigma^2

## Volatility = stddev(r)
	Implied volatility is determined using market prices of of a similar contract, by inverting the BSM formula

## MtM
	Value of the contract (uncertain+certain)

## Explained PnL
	Explains the part of the MtM caused by a move of an indicator

## VaR
	MtM loss (centered-scenario) in a portfolio, given a probability and a time horizon (usually 1-day @ 99%)

# Produits financiers
## Options
Soient
> q : taux de dividende
> r : taux d'interêt  sans risque
d1 = (ln(S/K) + (r-q+\sigma^2 /2) \* T) / (\sigma \sqrt T )
d1 = \frac {ln(S/ K)+(r-q+\sigma^2 /2) T}{\sigma \sqrt T}
d2 = d1 - \sigma \sqrt T 

Prime Call (S,T) = actu * Probabilité d'exercice * (Cours fwd * moneyness - K) = \exp-^rT  N(d2) (S\exp^rT  N(d1)/N(d2) - K)
Prime Put (S,T) = K \exp-^rT  N(-d2) - SN(-d1)

Delta call = (\exp^-qT) N(d1) &gt; 0
Delta put = (\exp^-qT) (N(d1)-1) &lt; 0

call - put = fwd
Delta call - Delta put = 1


# Bâle 
A set of recommandations on banking regulation

# Counterparty risk
OTC market
Measures:
* Capital requirement when lending money (credit/counterparty risk, market risk, operational risk)
* Price adjustement, taking into account the default probability of the counterparty

## CounterpartyExposure(t) = max(0, MtM(t))
MtM lost if the Counterparty defaults with 0 recovery rate

## Current exposure CE = CounterpartyExposure(0)

## Expected exposure EE(t) = E(CounterpartyExposure(t)) = avg(CE(t))

## Potential future exposure PFE(t,p) = maximum exposure at time t with a confidence of p
Simulated through Monte-Carlo
Unlike CVaR which takes the default probability into account

## Expected positive exposure = avg(EE(t), t=0 to 1Y)


