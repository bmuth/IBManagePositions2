﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using SWI.Controls;

namespace IBManagePositions
{
    class EWrapperImpl : EWrapper
    {
        LogCtl log;
        EClientSocket clientSocket;
        public readonly EReaderSignal Signal;

        public EWrapperImpl (LogCtl l)
        {
            log = l;
            Signal = new EReaderMonitorSignal ();
            clientSocket = new EClientSocket (this, Signal);
        }

        public EClientSocket ClientSocket
        {
            get
            {
                return clientSocket;
            }
            set
            {
                clientSocket = value;
            }
        }

        /* Delegates
         * --------- */

        public delegate void NextOrderIdEventHandler (int NextOrderId);
        public delegate void HistoricalDataEventHandler (int ReqId, Bar b);
        public delegate void HistoricalDataEndEventHandler (int ReqId, string StartDate, string EndDate);
        public delegate void Error1EventHandler (string s);
        public delegate void Error3EventHandler (int id, int errorCode, string errorMsg);
        public delegate void ContractDetailsEventHandler (int ReqId, ContractDetails c);
        public delegate void ContractDetailsEndEventHandler (int ReqId);
        public delegate void TickPriceEventHandler (int tickerId, int field, double price, TickAttrib attribs);
        public delegate void TickGenericEventHandler (int tickerId, int field, double value);
        public delegate void TickStringEventHandler (int tickerId, int field, string value);
        public delegate void TickSizeEventHandler (int tickerId, int field, int size);
        public delegate void TickEFPEventHandler (int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate);
        public delegate void TickOptionComputationEventHandler (int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice);

        /* Events
         * ------ */

        public event NextOrderIdEventHandler evNextValidId;
        public event HistoricalDataEventHandler evHistoricalData;
        public event HistoricalDataEndEventHandler evHistoricalDataEnd;
        public event Error1EventHandler evError1;
        public event Error3EventHandler evError3;
        public event ContractDetailsEventHandler evContractDetails;
        public event ContractDetailsEndEventHandler evContractDetailsEnd;
        public event TickPriceEventHandler evTickPrice;
        public event TickGenericEventHandler evTickGeneric;
        public event TickStringEventHandler evTickString;
        public event TickSizeEventHandler evTickSize;
        public event TickEFPEventHandler evTickEFP;
        public event TickOptionComputationEventHandler evTickOptionComputation;

        public int NextOrderId { get; set; }

        public void accountDownloadEnd (string account)
        {
            throw new NotImplementedException ();
        }

        public void accountSummary (int reqId, string account, string tag, string value, string currency)
        {
            throw new NotImplementedException ();
        }

        public void accountSummaryEnd (int reqId)
        {
            throw new NotImplementedException ();
        }

        public void accountUpdateMulti (int requestId, string account, string modelCode, string key, string value, string currency)
        {
            throw new NotImplementedException ();
        }

        public void accountUpdateMultiEnd (int requestId)
        {
            throw new NotImplementedException ();
        }

        public void bondContractDetails (int reqId, ContractDetails contract)
        {
            throw new NotImplementedException ();
        }

        public void commissionReport (CommissionReport commissionReport)
        {
            throw new NotImplementedException ();
        }

        public void completedOrder (Contract contract, IBApi.Order order, OrderState orderState)
        {
            throw new NotImplementedException ();
        }

        public void completedOrdersEnd ()
        {
            throw new NotImplementedException ();
        }

        public void connectAck ()
        {
            log.Log (ErrorLevel.logINF, "Connected to IB");
            if (ClientSocket.AsyncEConnect)
                ClientSocket.startApi ();
        }

        public void connectionClosed ()
        {
            log.Log (ErrorLevel.logINF, "Connection closed.");
        }

        public void contractDetails (int reqId, ContractDetails contractDetails)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.contractDetails {0}", reqId));
            evContractDetails?.Invoke (reqId, contractDetails);
        }

        public void contractDetailsEnd (int reqId)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.contractDetailsEnd {0}", reqId));
            evContractDetailsEnd?.Invoke (reqId);
        }

        public void currentTime (long time)
        {
            throw new NotImplementedException ();
        }

        public void deltaNeutralValidation (int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            throw new NotImplementedException ();
        }

        public void displayGroupList (int reqId, string groups)
        {
            throw new NotImplementedException ();
        }

        public void displayGroupUpdated (int reqId, string contractInfo)
        {
            throw new NotImplementedException ();
        }

        public void error (Exception e)
        {
            throw e;
        }

        public void error (string str)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl::error {0}", str));
            evError1?.Invoke (str);
        }

        public void error (int id, int errorCode, string errorMsg)
        {
            evError3?.Invoke (id, errorCode, errorMsg);
        }

        public void execDetails (int reqId, Contract contract, Execution execution)
        {
            throw new NotImplementedException ();
        }

        public void execDetailsEnd (int reqId)
        {
            throw new NotImplementedException ();
        }

        public void familyCodes (FamilyCode[] familyCodes)
        {
            throw new NotImplementedException ();
        }

        public void fundamentalData (int reqId, string data)
        {
            throw new NotImplementedException ();
        }

        public void headTimestamp (int reqId, string headTimestamp)
        {
            throw new NotImplementedException ();
        }

        public void histogramData (int reqId, HistogramEntry[] data)
        {
            throw new NotImplementedException ();
        }

        public void historicalData (int reqId, Bar bar)
        {
            log.Log (ErrorLevel.logDEB, "HistoricalData. " + reqId + " - Time: " + bar.Time + ", Open: " + bar.Open + ", High: " + bar.High + ", Low: " + bar.Low + ", Close: " + bar.Close + ", Volume: " + bar.Volume + ", Count: " + bar.Count + ", WAP: " + bar.WAP);
            evHistoricalData?.Invoke (reqId, bar);
        }

        public void historicalDataEnd (int reqId, string startDate, string endDate)
        {
            log.Log (ErrorLevel.logDEB, "HistoricalDataEnd - " + reqId + " from " + startDate + " to " + endDate);
            evHistoricalDataEnd?.Invoke (reqId, startDate, endDate);
        }

        public void historicalDataUpdate (int reqId, Bar bar)
        {
            throw new NotImplementedException ();
        }

        public void historicalNews (int requestId, string time, string providerCode, string articleId, string headline)
        {
            throw new NotImplementedException ();
        }

        public void historicalNewsEnd (int requestId, bool hasMore)
        {
            throw new NotImplementedException ();
        }

        public void historicalTicks (int reqId, HistoricalTick[] ticks, bool done)
        {
            throw new NotImplementedException ();
        }

        public void historicalTicksBidAsk (int reqId, HistoricalTickBidAsk[] ticks, bool done)
        {
            throw new NotImplementedException ();
        }

        public void historicalTicksLast (int reqId, HistoricalTickLast[] ticks, bool done)
        {
            throw new NotImplementedException ();
        }

        public void managedAccounts (string accountsList)
        {
            log.Log (ErrorLevel.logINF, "Account List:" + accountsList);
        }

        public void marketDataType (int reqId, int marketDataType)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.marketDataTypest: reqId={0} marketDataType={1}", reqId, marketDataType));
        }

        public void marketRule (int marketRuleId, PriceIncrement[] priceIncrements)
        {
            throw new NotImplementedException ();
        }

        public void mktDepthExchanges (DepthMktDataDescription[] depthMktDataDescriptions)
        {
            throw new NotImplementedException ();
        }

        public void newsArticle (int requestId, int articleType, string articleText)
        {
            throw new NotImplementedException ();
        }

        public void newsProviders (NewsProvider[] newsProviders)
        {
            throw new NotImplementedException ();
        }

        public void nextValidId (int orderId)
        {
            log.Log (ErrorLevel.logINF, "Next Valid Id: " + orderId);

            evNextValidId?.Invoke (NextOrderId);
        }

        public void openOrder (int orderId, Contract contract, IBApi.Order order, OrderState orderState)
        {
            throw new NotImplementedException ();
        }

        public void openOrderEnd ()
        {
            throw new NotImplementedException ();
        }

        public void orderBound (long orderId, int apiClientId, int apiOrderId)
        {
            throw new NotImplementedException ();
        }

        public void orderStatus (int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
        {
            throw new NotImplementedException ();
        }

        public void pnl (int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            throw new NotImplementedException ();
        }

        public void pnlSingle (int reqId, int pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            throw new NotImplementedException ();
        }

        public void position (string account, Contract contract, double pos, double avgCost)
        {
            throw new NotImplementedException ();
        }

        public void positionEnd ()
        {
            throw new NotImplementedException ();
        }

        public void positionMulti (int requestId, string account, string modelCode, Contract contract, double pos, double avgCost)
        {
            throw new NotImplementedException ();
        }

        public void positionMultiEnd (int requestId)
        {
            throw new NotImplementedException ();
        }

        public void realtimeBar (int reqId, long date, double open, double high, double low, double close, long volume, double WAP, int count)
        {
            throw new NotImplementedException ();
        }

        public void receiveFA (int faDataType, string faXmlData)
        {
            throw new NotImplementedException ();
        }

        public void rerouteMktDataReq (int reqId, int conId, string exchange)
        {
            throw new NotImplementedException ();
        }

        public void rerouteMktDepthReq (int reqId, int conId, string exchange)
        {
            throw new NotImplementedException ();
        }

        public void scannerData (int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            throw new NotImplementedException ();
        }

        public void scannerDataEnd (int reqId)
        {
            throw new NotImplementedException ();
        }

        public void scannerParameters (string xml)
        {
            throw new NotImplementedException ();
        }

        public void securityDefinitionOptionParameter (int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            throw new NotImplementedException ();
        }

        public void securityDefinitionOptionParameterEnd (int reqId)
        {
            throw new NotImplementedException ();
        }

        public void smartComponents (int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            throw new NotImplementedException ();
        }

        public void softDollarTiers (int reqId, SoftDollarTier[] tiers)
        {
            throw new NotImplementedException ();
        }

        public void symbolSamples (int reqId, ContractDescription[] contractDescriptions)
        {
            throw new NotImplementedException ();
        }

        public void tickByTickAllLast (int reqId, int tickType, long time, double price, int size, TickAttribLast tickAttriblast, string exchange, string specialConditions)
        {
            throw new NotImplementedException ();
        }

        public void tickByTickBidAsk (int reqId, long time, double bidPrice, double askPrice, int bidSize, int askSize, TickAttribBidAsk tickAttribBidAsk)
        {
            throw new NotImplementedException ();
        }

        public void tickByTickMidPoint (int reqId, long time, double midPoint)
        {
            throw new NotImplementedException ();
        }

        public void tickEFP (int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.tickEFP {0}", tickerId));
            evTickEFP?.Invoke (tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture, holdDays, futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
        }

        public void tickGeneric (int tickerId, int field, double value)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.tickGeneric {0}", tickerId));
            evTickGeneric?.Invoke (tickerId, field, value);
        }

        public void tickNews (int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            throw new NotImplementedException ();
        }

        public void tickOptionComputation (int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.tickOptionComputation {0}", tickerId));
            evTickOptionComputation?.Invoke (tickerId, field, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
        }

        public void tickPrice (int tickerId, int field, double price, TickAttrib attribs)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.tickPrice {0}", tickerId));
            evTickPrice?.Invoke (tickerId, field, price, attribs);
        }

        public void tickReqParams (int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.tickReqParams: tickerId={0} minTick={1} bboExchange={2} snapshotPermissions={3}", tickerId, minTick, bboExchange, snapshotPermissions));
        }

        public void tickSize (int tickerId, int field, int size)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.tickSize {0}", tickerId));
            evTickSize?.Invoke (tickerId, field, size);
        }

        public void tickSnapshotEnd (int tickerId)
        {
            throw new NotImplementedException ();
        }

        public void tickString (int tickerId, int field, string value)
        {
            log.Log (ErrorLevel.logDEB, string.Format ("EWrapperImpl.ticString {0}", tickerId));
            evTickString?.Invoke (tickerId, field, value);
        }

        public void updateAccountTime (string timestamp)
        {
            throw new NotImplementedException ();
        }

        public void updateAccountValue (string key, string value, string currency, string accountName)
        {
            throw new NotImplementedException ();
        }

        public void updateMktDepth (int tickerId, int position, int operation, int side, double price, int size)
        {
            throw new NotImplementedException ();
        }

        public void updateMktDepthL2 (int tickerId, int position, string marketMaker, int operation, int side, double price, int size, bool isSmartDepth)
        {
            throw new NotImplementedException ();
        }

        public void updateNewsBulletin (int msgId, int msgType, string message, string origExchange)
        {
            throw new NotImplementedException ();
        }

        public void updatePortfolio (Contract contract, double position, double marketPrice, double marketValue, double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            throw new NotImplementedException ();
        }

        public void verifyAndAuthCompleted (bool isSuccessful, string errorText)
        {
            throw new NotImplementedException ();
        }

        public void verifyAndAuthMessageAPI (string apiData, string xyzChallenge)
        {
            throw new NotImplementedException ();
        }

        public void verifyCompleted (bool isSuccessful, string errorText)
        {
            throw new NotImplementedException ();
        }

        public void verifyMessageAPI (string apiData)
        {
            throw new NotImplementedException ();
        }
    }
}
