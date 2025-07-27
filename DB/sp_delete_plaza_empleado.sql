SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Diego Castellanos
-- Create date: 20225 / 07 / 27
-- Description:	Eliminar empleados
-- =============================================
CREATE PROCEDURE sp_delete_plaza_empleado 
	-- Add the parameters for the stored procedure here
	@i_codigo int
	
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @d_codigo int, 
			@d_Puesto varchar(250),
			@d_Nombre varchar(250),
			@d_codigo_jefe int

	SELECT	TOP 1  
			@d_codigo = A.CODIGO,
			@d_Puesto = A.Puesto,
			@d_Nombre = A.Nombre,
			@d_codigo_jefe = A.codigo_jefe
	FROM	[tbl_plazas_empleados] A
	WHERE	codigo = @i_codigo


	IF (exists(SELECT CODIGO FROM [tbl_plazas_empleados] WHERE codigo_jefe = @i_codigo)) 
	BEGIN
		SELECT	@d_codigo AS CODIGO,
				@d_Puesto AS Puesto,
				@d_Nombre AS Nombre,
				@d_codigo_jefe AS codigo_jefe,
				'No se puede eliminar, Otros registros dependen de este.'AS MENSAJE
	END
	ELSE
	BEGIN
		DELETE FROM [dbo].[tbl_plazas_empleados] WHERE codigo = @i_codigo
		SELECT	@d_codigo AS CODIGO,
				@d_Puesto AS Puesto,
				@d_Nombre AS Nombre,
				@d_codigo_jefe AS codigo_jefe,
				'Datos Eliminados.' AS MENSAJE
	END

END
GO
