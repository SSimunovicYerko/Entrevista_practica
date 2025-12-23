import { useState } from "react";

type Props = {
  onCrear: (nombre: string, apellido: string, observacion: string) => Promise<void> | void;
  cargando: boolean;
};

export function ElementoForm(props: Props) {
  const [nombre, setNombre] = useState("");
  const [apellido, setApellido] = useState("");
  const [observacion, setObservacion] = useState("");

async function onSubmit(e: React.FormEvent) {
  e.preventDefault();

  await props.onCrear(nombre, apellido, observacion);

  setNombre("");
  setApellido("");
  setObservacion("");
}

  return (
    <form onSubmit={onSubmit} style={{ display: "flex", gap: 8 }}>
      <input
        value={nombre}
        onChange={(e) => setNombre(e.target.value)}
        placeholder="Nombre"
        style={{ flex: 1, padding: 8 }}
      />

      <input
        value={apellido}
        onChange={(e) => setApellido(e.target.value)}
        placeholder="Apellido"
        style={{ flex: 1, padding: 8 }}
      />

      <input
        value={observacion}
        onChange={(e) => setObservacion(e.target.value)}
        placeholder="Observacion"
        style={{ flex: 1, padding: 8 }}
      />

      <button type="submit" disabled={props.cargando} style={{ padding: "8px 12px" }}>
        Agregar
      </button>
    </form>
  );
}
