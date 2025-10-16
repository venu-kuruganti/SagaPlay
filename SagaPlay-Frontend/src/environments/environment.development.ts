export const environment = {
    production: false,
  // API gateway base URL (Api Gateway should be the public URL)
  apiBaseUrl: 'http://localhost:4000',

  // optional: per-service prefixes if you use them via gateway routes
  userServicePrefix: '/userservice',
  catalogServicePrefix: '/catalogservice',
  recommendationServicePrefix: '/recommendationservice',
  watchlistServicePrefix:'/watchlistservice'
};
