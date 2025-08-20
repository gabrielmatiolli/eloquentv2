import api from "@/lib/api";
import type Perk from "@/models/Perk";

/**
 * Busca todos os benefícios.
 * @returns {Promise<Perk[]>} Uma promessa que resolve para a lista de benefícios.
 */
export async function getAllPerks(): Promise<Perk[]> {
  try {
    const data = await api.get<Perk[]>("Perk");
    return data;
  } catch (error) {
    console.error("Falha ao buscar benefícios:", error);
    return [];
  }
}

/**
 * Busca um benefício específico pelo seu ID.
 * @param {string} id O ID do benefício.
 * @returns {Promise<Perk | null>} O benefício encontrado ou nulo.
 */
export async function getPerkById(id: string): Promise<Perk | null> {
  try {
    const data = await api.get<Perk>(`Perk/${id}`);
    return data;
  } catch (error) {
    console.error("Falha ao buscar benefício:", error);
    return null;
  }
}
