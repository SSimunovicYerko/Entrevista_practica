CREATE OR REPLACE PROCEDURE aplicacion.elementos_crear(
	IN p_nombre text,
	IN p_apellido text,
	IN p_observacion text,
	OUT p_id bigint)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
  INSERT INTO aplicacion.elementos (nombre, apellido, observacion)
  VALUES (p_nombre, p_apellido, p_observacion)
  RETURNING id INTO p_id;
END;
$BODY$;
ALTER PROCEDURE aplicacion.elementos_crear(text, text, text)
    OWNER TO postgres;

GRANT EXECUTE ON PROCEDURE aplicacion.elementos_crear(text, text, text) TO PUBLIC;

GRANT EXECUTE ON PROCEDURE aplicacion.elementos_crear(text, text, text) TO postgres;

GRANT EXECUTE ON PROCEDURE aplicacion.elementos_crear(text, text, text) TO usuario_app;