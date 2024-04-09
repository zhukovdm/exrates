FROM node:18.17-bookworm AS build
WORKDIR /opt/frontend/

COPY package*.json ./
RUN npm ci

COPY index.html ./
COPY app.js ./
RUN npm run build

FROM nginx:1.25.2-bookworm
WORKDIR /etc/nginx/

COPY ./config/nginx/default.conf ./conf.d/default.conf
COPY --from=build /opt/frontend/dist/ /usr/share/nginx/html/

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
