SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Diego Castellanos
-- Create date: 20225 / 07 / 27
-- Description:	Select empleados segun filtros
-- =============================================
CREATE PROCEDURE sp_select_plaza_empleado 
	-- Add the parameters for the stored procedure here
	@i_codigo int = NULL,
	@i_puesto varchar(250) = NULL,
	@i_nombre varchar(250) = NULL,
	@i_codigo_jefe int = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT	*
	FROM	[tbl_plazas_empleados] A
	WHERE	(@i_codigo IS NULL OR codigo = @i_codigo)
	AND		(@i_puesto IS NULL OR [Puesto] = @i_puesto)
	AND		(@i_nombre IS NULL OR [Nombre] = @i_nombre)
	AND		(@i_codigo_jefe IS NULL OR [codigo_jefe] = @i_codigo_jefe)

END
GO
