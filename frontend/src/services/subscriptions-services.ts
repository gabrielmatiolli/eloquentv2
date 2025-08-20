import type Subscription from "@/models/Subscription";
import api from "@/lib/api";

/**
 * Busca todas as assinaturas.
 * @returns {Promise<Subscription[]>} Uma promessa que resolve para a lista de assinaturas.
 */
export async function getAllSubscriptions(): Promise<Subscription[]> {
  try {
    const data = await api.get<Subscription[]>('Subscription');
    return data;
  } catch (error) {
    console.error("Falha ao buscar assinaturas:", error);
    return [];
  }
}

/**
 * Busca uma assinatura específica pelo seu ID.
 * @param {string} id O ID da assinatura.
 * @returns {Promise<Subscription | null>} A assinatura encontrada ou nulo.
 */
export async function getSubscriptionById(id: string): Promise<Subscription | null> {
  // ...lógica para buscar um único item
  return null;
}