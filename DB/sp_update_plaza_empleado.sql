SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Diego Castellanos
-- Create date: 20225 / 07 / 27
-- Description:	Actualizar empleados
-- =============================================
CREATE PROCEDURE sp_update_plaza_empleado 
	-- Add the parameters for the stored procedure here
	@i_codigo int,
	@i_puesto varchar(250) = NULL,
	@i_nombre varchar(250) = NULL,
	@i_codigo_jefe int = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	IF (@i_codigo_jefe <= 0) 
	BEGIN
		SET @i_codigo_jefe = NULL
	END

	UPDATE [dbo].[tbl_plazas_empleados]
		SET [Puesto] = @i_puesto
			,[Nombre] = @i_nombre
			,[codigo_jefe] = @i_codigo_jefe
		WHERE codigo = @i_codigo

	SELECT	* , 'Registro Actualizado' AS MENSAJE
	FROM	[tbl_plazas_empleados]
	WHERE	codigo = @i_codigo

END
GO
