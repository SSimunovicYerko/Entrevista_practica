import type { CrearElementoRequest, Elemento } from "../types/elemento";

const baseUrl = import.meta.env.VITE_API_URL;

async function leerError(res: Response): Promise<string> {
  try {
    const data = await res.json();
    if (data?.mensaje) return String(data.mensaje);
  } catch {
  }
  return `Error HTTP ${res.status}`;
}

export async function obtenerElementos(): Promise<Elemento[]> {
  const res = await fetch(`${baseUrl}/api/elementos`);
  if (!res.ok) throw new Error(await leerError(res));
  return await res.json();
}

export async function crearElemento(req: CrearElementoRequest): Promise<{ id: number }> {
  const res = await fetch(`${baseUrl}/api/elementos`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(req),
  });

  if (!res.ok) throw new Error(await leerError(res));
  return await res.json();
}
