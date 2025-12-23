export type Elemento = {
  id: number;
  nombre: string;
  apellido: string;
  observacion: string;
  creadoEn: string;
};

export type CrearElementoRequest = {
  nombre: string;
  apellido: string;
  observacion: string;
};
