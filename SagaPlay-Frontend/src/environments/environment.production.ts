export const environment = {
    production: true,
  // API gateway base URL (Api Gateway should be the public URL)
  apiBaseUrl: 'https://apigateway-service.happysand-26d752fc.southindia.azurecontainerapps.io',

  // optional: per-service prefixes if you use them via gateway routes
  userServicePrefix: '/userservice',
  catalogServicePrefix: '/catalogservice',
  recommendationServicePrefix: '/recommendationservice',
  watchlistServicePrefix:'/watchlistservice'
};
