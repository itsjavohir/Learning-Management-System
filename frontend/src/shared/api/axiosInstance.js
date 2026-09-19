import axios from 'axios';
import { tokenStorage } from '../lib/tokenStorage';

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: { 'Content-Type': 'application/json' },
});

axiosInstance.interceptors.request.use((config) => {
  const accessToken = tokenStorage.getAccessToken();
  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }
  return config;
});

axiosInstance.interceptors.response.use(
    (response) => response,
    async (error) => {
      const originalRequest = error.config;

      const isAuthEndpoint = originalRequest?.url?.includes('/auth/login')
          || originalRequest?.url?.includes('/auth/refresh-token')
          || originalRequest?.url?.includes('/auth/forgot-password')
          || originalRequest?.url?.includes('/auth/reset-password');

      if (error.response?.status === 401 && !originalRequest._retry && !isAuthEndpoint) {
        originalRequest._retry = true;

        try {
          const refreshToken = tokenStorage.getRefreshToken();
          const { data } = await axios.post(
              `${import.meta.env.VITE_API_URL}/auth/refresh-token`,
              { refreshToken }
          );

          tokenStorage.setTokens(data.accessToken, data.refreshToken);
          originalRequest.headers.Authorization = `Bearer ${data.accessToken}`;
          return axiosInstance(originalRequest);
        } catch {
          tokenStorage.clearTokens();
          window.location.href = '/login';
          return Promise.reject(error);
        }
      }

      return Promise.reject(error);
    }
);

export default axiosInstance;