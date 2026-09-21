import axiosInstance from '../../../shared/api/axiosInstance';

export const courseApi = {
    getAll: async () => {
        const response = await axiosInstance.get('/courses');
        return response.data;
    },

    getById: async (id) => {
        const response = await axiosInstance.get(`/courses/${id}`);
        return response.data;
    },

    create: async (courseData) => {
        const response = await axiosInstance.post('/courses', courseData);
        return response.data;
    },

    update: async ({ id, ...courseData }) => {
        const response = await axiosInstance.put(`/courses/${id}`, courseData);
        return response.data;
    },

    // TODO: confirm delete payload with backend (assumed { id })
    delete: async (id) => {
        await axiosInstance.delete('/courses', { data: { id } });
    },
};
