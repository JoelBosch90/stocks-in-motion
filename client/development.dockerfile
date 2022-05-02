# Get a light NodeJS base image.
FROM node:17-alpine

# Create the working directory.
WORKDIR /client

# Copy the package files that contain our dependencies.
COPY package*.json ./

# We can switch to development mode.
ENV NODE_ENV development

# Install all dependencies.
RUN npm install

# Copy all files.
COPY . .

# Start the service.
CMD ["npm", "run", "dev"]