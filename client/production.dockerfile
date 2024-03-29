# Get a light NodeJS base image.
FROM node:17-alpine as base
WORKDIR /base

# Copy the package files that contain our dependencies.
COPY package*.json ./

# Install all dependencies.
RUN npm install

# Copy all files.
COPY . .


# Time to build the production image.
FROM base AS build
WORKDIR /build

# We can switch to production mode.
ENV NODE_ENV production

# Copy over the base files.
COPY --from=base /base ./

# Create the production build.
RUN npm run build


# Start a fresh release image.
FROM node:17-alpine
WORKDIR /release

# We can switch to production mode.
ENV NODE_ENV production

# Copy all files we need, but only the files that we need.
COPY --from=build /build/package*.json ./
COPY --from=build /build/.next ./.next
COPY --from=build /build/public ./public
COPY --from=build /build/node_modules ./node_modules
COPY --from=build /build/next.config.js ./

# Start the service.
CMD ["npm", "start"]