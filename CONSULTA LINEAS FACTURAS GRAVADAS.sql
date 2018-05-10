use DVINVERSIONES
go

declare @DocEntry int
declare @DocNum int
declare @lineas table (
	Campo1 nchar(4),
	Campo2 varchar(5),
	Campo3 varchar(10),
	Campo4 varchar(250),
	Campo5 numeric(12,2),
	Campo6 numeric(12,2),
	Campo7 numeric(12,2),
	Campo8 numeric(12,2),
	Campo9 numeric(12,2),
	Campo10 integer,
	Campo11 numeric(12,2),
	Campo12 numeric(12,2),
	Campo13 varchar(5),
	Campo14 varchar(4),
	Campo15 integer
)

set @DocNum = 542
set @DocEntry = (select DocEntry from OINV where DocNum = @DocNum)

select 'item','NIU',t0.ItemCode,t0.Dscription,convert(numeric(12,2),round(t0.Quantity,2))as'cantidad',Convert(numeric(12,2),round(t0.PriceBefDi,2))as'val uni',
		convert(numeric(12,2),round(t0.PriceBefDi*((t0.VatPrcnt/100)+1),2))as'precio',
		convert(numeric(12,2),round(((t0.DiscPrcnt/100)*(t0.Quantity*t0.PriceBefDi)),2))as'Descuento',
		convert(numeric(12,2),(t0.Price*t0.Quantity))as'subtot',1as'tip igv', convert(numeric(12,2),t0.VatSum) as'tot igv',
		convert(numeric(12,2),(t0.Price*t0.Quantity)+t0.VatSum),'false',null,null
	from INV1 t0
	where DocEntry = @DocEntry

select * from INV1 where DocEntry = @DocEntry