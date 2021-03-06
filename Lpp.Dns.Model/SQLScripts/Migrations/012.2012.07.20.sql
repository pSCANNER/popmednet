if exists( select * from sys.foreign_keys where 
	parent_object_id = object_id('dbo.queriesdatamarts') and name = 'FK_QueriesDataMarts_QueryStatusTypes' )
alter table queriesdatamarts drop constraint FK_QueriesDataMarts_QueryStatusTypes
go

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RoutingCounts]'))  drop view dbo.RoutingCounts
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[_RoutingCounts]'))  drop view dbo._RoutingCounts
go

set ansi_padding on
go

create view _RoutingCounts
with schemabinding
as
select QueryId,
	Sum(case when QueryStatusTypeId = 2 then 1 else 0 end) as Submitted,
	Sum(case when QueryStatusTypeId = 3 then 1 else 0 end) as Completed,
	Sum(case when QueryStatusTypeId = 4 then 1 else 0 end) as AwaitingRequestApproval,
	Sum(case when QueryStatusTypeId = 10 then 1 else 0 end) as AwaitingResponseApproval,
	Sum(case when QueryStatusTypeId = 5 then 1 else 0 end) as RejectedRequest,
	Sum(case when QueryStatusTypeId = 12 then 1 else 0 end) as RejectedBeforeUploadResults,
	Sum(case when QueryStatusTypeId = 13 then 1 else 0 end) as RejectedAfterUploadResults,
	COUNT_BIG(*) as Total
from
	dbo.QueriesDataMarts
group by QueryId
go

create unique clustered index pk_ix on _RoutingCounts(QueryId) 
go

create index ix on _RoutingCounts(QueryId, Submitted, Completed, AwaitingRequestApproval, AwaitingResponseApproval, Total, 
	RejectedRequest, RejectedBeforeUploadResults, RejectedAfterUploadResults)
go

create view RoutingCounts as select * from _RoutingCounts with(noexpand)
go