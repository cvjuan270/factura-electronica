
use DVINVERSIONES
go
/*
DECLARE @operacion nchar (12)
DECLARE @tipo_de_comprobante Integer
DECLARE @serie nchar (4)
DECLARE @numero Integer
DECLARE @sunat_transaction Integer
DECLARE @cliente_tipo_de_documento Integer
DECLARE @cliente_numero_de_documento nchar (15)
DECLARE @cliente_denominacion nvarchar (100)
DECLARE @cliente_direccion nvarchar (100)
DECLARE @cliente_email nvarchar (250)
DECLARE @cliente_email_1 nvarchar (250)
DECLARE @cliente_email_2 nvarchar (250)
DECLARE @fecha_de_emision date
DECLARE @fecha_de_vencimiento date
DECLARE @moneda Integer
DECLARE @tipo_de_cambio numeric (2,2)
DECLARE @porcentaje_de_igv numeric (2,2)
DECLARE @descuento_global numeric (12,2)
DECLARE @total_descuento numeric (12,2)
DECLARE @total_anticipo numeric (12,2)
DECLARE @total_gravada numeric (12,2)
DECLARE @total_inafecta numeric (12,2)
DECLARE @total_exonerada numeric (12,2)
DECLARE @total_igv numeric (12,2)
DECLARE @total_gratuita numeric (12,2)
DECLARE @total_otros_cargos numeric (12,2)
DECLARE @total numeric (12,2)
DECLARE @percepcion_tipo Integer
DECLARE @percepcion_base_imponible numeric (12,2)
DECLARE @total_percepcion numeric (12,2)
DECLARE @total_incluido_percepcion numeric (12,2)
DECLARE @detraccion nchar (5)
DECLARE @observaciones nvarchar (5)
DECLARE @documento_que_se_modifica_tipo Integer
DECLARE @documento_que_se_modifica_serie nchar (4)
DECLARE @documento_que_se_modifica_numero nchar (8)
DECLARE @tipo_de_nota_de_credito Integer
DECLARE @tipo_de_nota_de_debito Integer
DECLARE @enviar_automaticamente_a_la_sunat nchar (5)
DECLARE @enviar_automaticamente_al_cliente nchar (5)
DECLARE @codigo_unico nvarchar (20)
DECLARE @condiciones_de_pago nvarchar (250)
DECLARE @medio_de_pago nvarchar (250)
DECLARE @placa_vehiculo nvarchar (8)
DECLARE @orden_compra_servicio nvarchar (20)
DECLARE @tabla_personalizada_codigo nvarchar (250)
DECLARE @formato_de_pdf nvarchar (5)
*/

DECLARE @FACTURA_NC_ND_BOLETA TABLE (
	operacion nchar (12),
	tipo_de_comprobante Integer,
	serie nchar (4),
	numero Integer,
	sunat_transaction Integer,
	cliente_tipo_de_documento Integer,
	cliente_numero_de_documento nchar (15),
	cliente_denominacion nvarchar (100),
	cliente_direccion nvarchar (100),
	cliente_email nvarchar (250),
	cliente_email_1 nvarchar (250),
	cliente_email_2 nvarchar (250),
	fecha_de_emision date,
	fecha_de_vencimiento date,
	moneda Integer,
	tipo_de_cambio numeric (2,2),
	porcentaje_de_igv numeric (2,2),
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

	
	SELECT 'Generar_comprobante',1,t0.FolioPref,t0.FolioNum,1,6,t0.LicTradNum,t0.CardName,t0.Address,
			T1.E_Mail,'','',convert(varchar,t0.DocDate,(103)),convert(varchar,t0.DocDueDate,(103))
	FROM OINV t0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode
	WHERE DocNum = 3610
