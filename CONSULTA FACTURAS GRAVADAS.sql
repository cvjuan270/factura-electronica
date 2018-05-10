
use DVINVERSIONES
go
DECLARE @FILAS TABLE(CAMPO1 VARCHAR(50))
INSERT INTO @FILAS 
VALUES 
('operacion'),
('tipo_de_comprobante'),
('serie'),
('numero'),
('sunat_transaction'),
('cliente_tipo_de_documento'),
('cliente_numero_de_documento'),
('cliente_denominacion'),
('cliente_direccion'),
('cliente_email'),
('cliente_email_1'),
('cliente_email_2'),
('fecha_de_emision'),
('fecha_de_vencimiento'),
('moneda'),
('tipo_de_cambio'),
('porcentaje_de_igv'),
('descuento_global'),
('total_descuento'),
('total_anticipo'),
('total_gravada'),
('total_inafecta'),
('total_exonerada'),
('total_igv'),
('total_gratuita'),
('total_otros_cargos'),
('total'),
('percepcion_tipo'),
('percepcion_base_imponible'),
('total_percepcion'),
('total_incluido_percepcion'),
('detraccion'),
('observaciones'),
('documento_que_se_modifica_tipo'),
('documento_que_se_modifica_serie'),
('documento_que_se_modifica_numero'),
('tipo_de_nota_de_credito'),
('tipo_de_nota_de_debito'),
('enviar_automaticamente_a_la_sunat'),
('enviar_automaticamente_al_cliente'),
('codigo_unico'),
('condiciones_de_pago'),
('medio_de_pago'),
('placa_vehiculo'),
('orden_compra_servicio'),
('tabla_personalizada_codigo'),
('formato_de_pdf');



DECLARE @FACTURA_NC_ND_BOLETA TABLE (
	operacion varchar (19),
	tipo_de_comprobante Integer,
	serie varchar (4),
	numero Integer,
	sunat_transaction Integer,
	cliente_tipo_de_documento Integer,
	cliente_numero_de_documento nchar (12),
	cliente_denominacion varchar (100),
	cliente_direccion varchar (100),
	cliente_email varchar (250),
	cliente_email_1 varchar (250),
	cliente_email_2 varchar (250),
	fecha_de_emision date,
	fecha_de_vencimiento date,
	moneda Integer,
	tipo_de_cambio numeric (12,2),
	porcentaje_de_igv numeric (12,2),
	descuento_global numeric (12,2),
	total_descuento numeric (12,2),
	total_anticipo numeric (12,2),
	total_gravada numeric (12,2),
	total_inafecta numeric (12,2),
	total_exonerada numeric (12,2),
	total_igv numeric (12,2),
	total_gratuita numeric (12,2),
	total_otros_cargos numeric (12,2),
	total numeric (12,2),
	percepcion_tipo Integer,
	percepcion_base_imponible numeric (12,2),
	total_percepcion numeric (12,2),
	total_incluido_percepcion numeric (12,2),
	detraccion nchar (5),
	observaciones nvarchar (5),
	documento_que_se_modifica_tipo Integer,
	documento_que_se_modifica_serie nchar (4),
	documento_que_se_modifica_numero nchar (8),
	tipo_de_nota_de_credito Integer,
	tipo_de_nota_de_debito Integer,
	enviar_automaticamente_a_la_sunat nchar (5),
	enviar_automaticamente_al_cliente nchar (5),
	codigo_unico nvarchar (20),
	condiciones_de_pago nvarchar (250),
	medio_de_pago nvarchar (250),
	placa_vehiculo nvarchar (8),
	orden_compra_servicio nvarchar (20),
	tabla_personalizada_codigo nvarchar(250),
	formato_de_pdf nvarchar(5)
	)
	/*NUMERO DE DOCUMENTO*/
	DECLARE @DOCNUM INT
	SET @DOCNUM = 3610

	/*MONEDA*/

	declare @MONEDA nchar(5)
	declare @MONEDATXT integer

	SET @MONEDA = (SELECT DocCur FROM OINV WHERE DocNum = @DOCNUM)
	IF(@MONEDA = 'S/')
		BEGIN
		SET @MONEDATXT = 1
		END
		ELSE
		BEGIN
		SET @MONEDATXT=2
		END
	/********************/

	--SELECT * FROM OINV WHERE DocNum = @DOCNUM
	INSERT INTO @FACTURA_NC_ND_BOLETA

	SELECT 'Generar_comprobante',1,t0.FolioPref,t0.FolioNum,1,6,t0.LicTradNum,t0.CardName,t0.Address,
			T1.E_Mail,'','',convert(varchar,t0.DocDate,(103)),convert(varchar,t0.DocDueDate,(103)),@MONEDATXT,
			convert(numeric,T0.DocRate),18.00,convert(numeric(12,2),t0.DiscSum),convert(numeric(12,2),t0.DiscSum),0.00,convert(numeric(12,2),t0.DocTotal-(VatSum-DiscSum)),
			0.00,0.00,convert(numeric(12,2),t0.VatSum),0.00,0.00,convert(numeric(12,2),t0.DocTotal),null,null,null,null,null,null,null,null,null,null,null,
			'true','false',null,t2.PymntGroup,null,null,t0.NumAtCard,null,null

	FROM OINV t0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode
		inner join OCTG t2 on t2.GroupNum = t0.GroupNum
	WHERE DocNum = @DOCNUM

/*****************************/
SELECT * FROM @FILAS
SELECT * FROM @FACTURA_NC_ND_BOLETA

