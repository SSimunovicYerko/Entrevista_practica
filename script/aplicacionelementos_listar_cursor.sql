CREATE OR REPLACE PROCEDURE aplicacion.elementos_listar_cursor(
	OUT p_cursor refcursor)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
  p_cursor := 'cur_elementos'; 
  OPEN p_cursor FOR
    SELECT id, nombre,apellido,observacion, creado_en
    FROM aplicacion.elementos
    ORDER BY id;
END;
$BODY$;
ALTER PROCEDURE aplicacion.elementos_listar_cursor()
    OWNER TO postgres;

GRANT EXECUTE ON PROCEDURE aplicacion.elementos_listar_cursor() TO PUBLIC;

GRANT EXECUTE ON PROCEDURE aplicacion.elementos_listar_cursor() TO postgres;

GRANT EXECUTE ON PROCEDURE aplicacion.elementos_listar_cursor() TO usuario_app;
