### STAGE 1: Build ###
FROM node:15.14.0-alpine3.10 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run buildprod
### STAGE 2: Run ###
FROM nginx:1.19.9
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/web-vote-client /usr/share/nginx/html
EXPOSE 80