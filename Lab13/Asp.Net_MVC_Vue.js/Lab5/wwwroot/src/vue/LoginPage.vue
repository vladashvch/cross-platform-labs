<script>
    export default {
        name: "LoginPage",

        data() {
            return {
                name: "LoginPage"
            };
        },

        mounted() {
            const loginForm = document.getElementById('loginForm');
            if (loginForm) {
                loginForm.addEventListener('submit', this.handleSubmit);
            }
        },

        methods: {
            async handleSubmit(event) {
                event.preventDefault();
                const username = document.getElementById('username').value;
                const password = document.getElementById('password').value;

                try {
                    const response = await fetch('/account/loginAuth', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ username, password })
                    });

                    const data = await response.json();
                    if (data.token) {
                        localStorage.setItem('token', data.token);
                        window.location.href = '/home';
                    } else {
                        console.error('Token not found in response:', data);
                    }
                } catch (error) {
                    console.error('Login failed:', error);
                }
            }
        },

        beforeUnmount() {
            const loginForm = document.getElementById('loginForm');
            if (loginForm) {
                loginForm.removeEventListener('submit', this.handleSubmit);
            }
        }
    };
</script>

<template>
    <div>
        <h1 class="text-center">Login</h1>
        <section>
            <form id="loginForm">
                <label for="username">��'� �����������:</label>
                <input type="text" id="username" name="username" required>

                <label for="password">������:</label>
                <input type="password" id="password" name="password" required>

                <button type="submit">�����</button>
            </form>
        </section>
    </div>
</template>