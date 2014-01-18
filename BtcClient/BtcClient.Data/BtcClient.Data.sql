drop table BitcoinChartHistory;
--create table BitcoinChartHistory(Market text not null, Now datetime not null, Price double not null, Amount double not null, PRIMARY KEY (Market,Now desc));
create table BitcoinChartHistory(id INTEGER PRIMARY KEY AUTOINCREMENT, Market text not null, Now datetime not null, Price double not null, Amount double not null);
create index BitcoinChartHistory_idx on BitcoinChartHistory(Market, Now desc);
