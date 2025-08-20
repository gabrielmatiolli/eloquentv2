export class ApiError extends Error {
  constructor(message: string, public status: number) {
    super(message);
    this.name = 'ApiError';
  }
}

const api = {
  get: async <T>(endpoint: string, options?: RequestInit): Promise<T> => {
    const url = `http://172.18.0.3:8080/api/${endpoint}`;

    const response = await fetch(url, {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        // 'Authorization': `Bearer ${token}`, // Exemplo de como adicionar um token
        ...options?.headers,
      },
    });

    if (!response.ok) {
      throw new ApiError(`Erro na requisição para ${endpoint}`, response.status);
    }

    return response.json() as Promise<T>;
  },
  
  // Você pode adicionar métodos post, put, delete, etc. aqui
  // post: async <T>(endpoint: string, body: any, options?: RequestInit): Promise<T> => { ... }
};

export default api;