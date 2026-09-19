import axiosInstance from '../../../shared/api/axiosInstance';

export const AUTH_CHANNEL = {
    EMAIL: 1,
    TELEGRAM: 2,
};

export const authApi = {
    login: async (phoneNumber, password) => {
        const response = await axiosInstance.post('/auth/login', { phoneNumber, password });
        return response.data;
    },

    changePassword: async (oldPassword, newPassword, confirmPassword) => {
        const response = await axiosInstance.post(
            '/auth/change-password',
            {
                oldPassword,
                newPassword,
                confirmPassword
            }
        );

        return response.data;
    },

    // channel: AUTH_CHANNEL.EMAIL | AUTH_CHANNEL.TELEGRAM
    // only the field matching the channel is sent to the backend
    forgotPassword: async ({ channel, phoneNumber, email }) => {
        const payload = { channel };

        if (channel === AUTH_CHANNEL.TELEGRAM) {
            payload.phoneNumber = phoneNumber;
        } else {
            payload.email = email;
        }

        const response = await axiosInstance.post('/auth/forgot-password', payload);
        return response.data;
    },

    // same channel rule applies here: send back whichever field the code was requested with
    resetPassword: async ({ channel, phoneNumber, email, verifyCode, newPassword, confirmPassword }) => {
        const payload = { channel, verifyCode, newPassword, confirmPassword };

        if (channel === AUTH_CHANNEL.TELEGRAM) {
            payload.phoneNumber = phoneNumber;
        } else {
            payload.email = email;
        }

        const response = await axiosInstance.post('/auth/reset-password', payload);
        return response.data;
    },
};
