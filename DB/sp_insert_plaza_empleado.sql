SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Diego Castellanos
-- Create date: 20225 / 07 / 27
-- Description:	Insertar nuevos empleados
-- =============================================
CREATE PROCEDURE sp_insert_plaza_empleado 
	-- Add the parameters for the stored procedure here
	@i_puesto varchar(250) = NULL,
	@i_nombre varchar(250) = NULL,
	@i_codigo_jefe int = NULL
AS
BEGIN
	
	IF (@i_codigo_jefe <= 0) 
	BEGIN
		SET @i_codigo_jefe = NULL
	END

	SET NOCOUNT ON;

	INSERT INTO [dbo].[tbl_plazas_empleados]
				([Puesto]
				,[Nombre]
				,[codigo_jefe])
			VALUES
				(@i_puesto
				,@i_nombre
				,@i_codigo_jefe)

	SELECT	A.* ,  'Registro Insertado'  AS MENSAJE
	FROM	tbl_plazas_empleados A
	WHERE	A.CODIGO = SCOPE_IDENTITY()
END
GO
