# Get a light NodeJS base image.
FROM node:17-alpine

# Create the working directory and give ownership to the node user.
RUN mkdir -p /client && chown -R node:node /client

# Create the working directory.
WORKDIR /client

# We can switch to production mode.
ENV NODE_ENV production

# Use the node user to run the install commands.
USER node

# Copy the package files that contain our dependencies.
COPY --chown=node:node package*.json ./

# Install all dependencies.
RUN npm install

# Copy all files.
COPY --chown=node:node . .

# Create the production build.
RUN npm run build

# Start the service.
CMD ["npm", "start"]