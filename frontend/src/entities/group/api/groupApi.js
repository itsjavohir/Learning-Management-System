import axiosInstance from '../../../shared/api/axiosInstance';

export const groupApi = {
    getAll: async () => {
        const response = await axiosInstance.get('/groups');
        return response.data;
    },

    getById: async (id) => {
        const response = await axiosInstance.get(`/groups/${id}`);
        return response.data;
    },

    create: async (groupData) => {
        const response = await axiosInstance.post('/groups', groupData);
        return response.data;
    },

    update: async ({ id, ...groupData }) => {
        const response = await axiosInstance.put(`/groups/${id}`, groupData);
        return response.data;
    },

    delete: async (id) => {
        await axiosInstance.delete(`/groups/${id}`);
    },
};
