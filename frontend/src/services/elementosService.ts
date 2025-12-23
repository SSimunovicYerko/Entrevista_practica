import type { Elemento } from "../types/elemento";
import { crearElemento, obtenerElementos } from "../api/elementosApi";

export async function listarElementos(): Promise<Elemento[]> {
  return await obtenerElementos();
}

export async function agregarElemento(nombre: string, apellido: string, observacion:string ): Promise<{ id: number }> {
  const nombreLimpio = nombre.trim();
  const apellidoLimpio = apellido.trim();
  const observacionLimpia = observacion.trim();

  if (!nombreLimpio) throw new Error("El nombre es obligatorio");
  if (!apellidoLimpio) throw new Error("El apellido es obligatorio");
  if (!observacionLimpia) throw new Error("La observación es obligatoria");

  if (nombreLimpio.length > 100) throw new Error("El nombre no puede superar 100 caracteres");
  if (apellidoLimpio.length > 100) throw new Error("El apellido no puede superar 100 caracteres");
  if (observacionLimpia.length > 500) throw new Error("La observación no puede superar 500 caracteres");

  return await crearElemento({
    nombre: nombreLimpio,
    apellido: apellidoLimpio,
    observacion: observacionLimpia,
  });
}
