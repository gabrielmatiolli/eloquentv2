# Estágio 'deps' (será nosso alvo para desenvolvimento)
# Renomeamos para 'development' para ficar mais claro
FROM node:20-alpine AS development
WORKDIR /app
COPY package.json yarn.lock* package-lock.json* pnpm-lock.yaml* ./
RUN \
  if [ -f yarn.lock ]; then yarn install --frozen-lockfile; \
  elif [ -f package-lock.json ]; then npm ci; \
  elif [ -f pnpm-lock.yaml ]; then yarn global add pnpm && pnpm i; \
  else echo "Lockfile not found." && exit 1; \
  fi
# O código será montado via volume e o comando 'npm run dev' virá do override

# --- Estágios de Produção (permanecem os mesmos) ---

# Estágio 'builder'
FROM node:20-alpine AS builder
WORKDIR /app
COPY --from=development /app/node_modules ./node_modules
COPY . .
ENV NEXT_PUBLIC_API_URL=http://localhost:8080
RUN npm run build

# Estágio 'runner'
FROM node:20-alpine AS runner
WORKDIR /app
ENV NODE_ENV=production
COPY --from=builder /app/public ./public
COPY --from=builder --chown=nextjs:nodejs /app/.next ./.next
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/package.json ./package.json
EXPOSE 3000
CMD ["npm", "start"]