import type { Elemento } from "../types/elemento";

type Props = {
  elementos: Elemento[];
};

export function ElementoList(props: Props) {
  return (
    <ul style={{ marginTop: 16 }}>
      {props.elementos.map((x) => (
        <li key={x.id}>
          <strong>#{x.id}</strong> - {x.nombre} {x.apellido} <br />
          <span>{x.observacion}</span> <br />
          <small>{x.creadoEn}</small>
        </li>
      ))}
    </ul>
  );
}
