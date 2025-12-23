import { useEffect, useState } from "react";
import type { Elemento } from "../types/elemento";
import { agregarElemento, listarElementos } from "../services/elementosService";
import { ElementoForm } from "../components/ElementoForm";
import { ElementoList } from "../components/ElementoList";

export function ElementosPage() {
  const [elementos, setElementos] = useState<Elemento[]>([]);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState("");

  async function cargar() {
    setError("");
    setCargando(true);
    try {
      const data = await listarElementos();
      setElementos(data);
    } catch (e: any) {
      setError(e?.message ?? "Error");
    } finally {
      setCargando(false);
    }
  }

  useEffect(() => {
    cargar();
  }, []);

  async function crear(nombre: string, apellido: string, observacion: string) {
    setError("");
    setCargando(true);
    try {
      await agregarElemento(nombre,apellido,observacion);
      await cargar();
    } catch (e: any) {
      setError(e?.message ?? "Error");
    } finally {
      setCargando(false);
    }
  }

  return (
    <div style={{ maxWidth: 650, margin: "40px auto", fontFamily: "Arial" }}>
      <h2>Elementos</h2>

      <ElementoForm onCrear={crear} cargando={cargando} />

      {error ? <p style={{ marginTop: 12 }}>{error}</p> : null}
      {cargando ? <p style={{ marginTop: 12 }}>Cargando...</p> : null}

      <ElementoList elementos={elementos} />
    </div>
  );
}
