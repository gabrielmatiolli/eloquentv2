interface Dados {
  id: number;
  nome: string;
  price: number;
  beneficios: {
    contas: number;
    previsao: number;
    alertas: boolean;
    historico: number;
    aplicativo: boolean;
    temporeal?: boolean;
    customizacao: boolean;
  };
}

export const dados: Dados[] = [
  {
    id: 1,
    nome: "Individual",
    price: 25,
    beneficios: {
      contas: 1,
      previsao: 7,
      alertas: true,
      historico: 14,
      aplicativo: false,
      customizacao: true,
    },
  },
  {
    id: 2,
    nome: "Shared",
    price: 50,
    beneficios: {
      contas: 2,
      previsao: 14,
      alertas: true,
      historico: 30,
      temporeal: true,
      aplicativo: true,
      customizacao: true,
    },
  },
  {
    id: 3,
    nome: "Family",
    price: 100,
    beneficios: {
      contas: 5,
      previsao: 30,
      alertas: true,
      historico: 60,
      aplicativo: true,
      customizacao: true,
      temporeal: true,
    },
  },
  {
    id: 4,
    nome: "Enterprise",
    price: 200,
    beneficios: {
      contas: 100,
      previsao: 365,
      alertas: true,
      historico: 365,
      aplicativo: true,
      temporeal: true,
      customizacao: true,
    },
  },
];