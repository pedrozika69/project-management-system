# Stage 1
# Build My Frontend Framework
FROM node:lts-alpine AS builder

WORKDIR /app

COPY ./app/package*.json /app/

RUN npm install

COPY ./app /app

RUN npm run build

# Stage 2
FROM nginx:alpine
# I will copy the content from builder stage to nginx public folder
COPY --from=builder /app/dist /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]