<script>
    export default {
        name: "ProfilePage",

        data() {
            return {
                token: localStorage.getItem('token'),
                userData: null,
                errorMessage: null,
                isLoading: true,
            };
        },

        mounted() {
            if (!this.token) {
                this.redirectToLogin();
                return;
            }
            this.fetchUserProfile();
        },

        methods: {
            async fetchUserProfile() {
                try {
                    const response = await fetch('/account/get-profile', {
                        method: 'GET',
                        headers: {
                            'Authorization': `Bearer ${this.token}`
                        }
                    });

                    if (!response.ok) {
                        const errorData = await response.json();
                        throw new Error(errorData.message || 'Failed to fetch profile');
                    }

                    this.userData = await response.json();
                    this.isLoading = false;
                } catch (error) {
                    this.errorMessage = error.message;
                    this.isLoading = false;

                    if (error.message.includes('Unauthorized')) {
                        localStorage.removeItem('token');
                        setTimeout(this.redirectToLogin, 2000);
                    }
                }
            },

            redirectToLogin() {
                window.location.href = '/login';
            }
        }
    };
</script>

<template>
    <div>
        <h1>��� �������</h1>

        <div v-if="isLoading">������������...</div>

        <div v-if="!isLoading && userData">
            <p><strong>��'� �����������:</strong> <span>{{ userData.nickname || '�� �������' }}</span></p>
            <p><strong>Բ�:</strong> <span>{{ userData.name || '�� �������' }}</span></p>
            <p><strong>�������:</strong> <span>{{ userData.phone || '�� �������' }}</span></p>
            <p><strong>���������� ������:</strong> <span>{{ userData.email || '�� �������' }}</span></p>
        </div>

        <div v-if="errorMessage" class="alert alert-danger">
            {{ errorMessage }}
        </div>
    </div>
</template>