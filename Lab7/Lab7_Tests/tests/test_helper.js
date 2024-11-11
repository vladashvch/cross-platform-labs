'use strict';

require('dotenv').config();
const axios = require('axios');
const qs = require('qs');

const DOMAIN = process.env.DOMAIN;
const CLIENT_ID = process.env.CLIENT_ID;
const CLIENT_SECRET = process.env.CLIENT_SECRET;
const AUDIENCE = `https://${DOMAIN}/api/v2/`;
const LOGIN = process.env.LOGIN;
const PASSWORD = process.env.PASSWORD;

const getUserTokens = async () => {
    const options = {
        method: 'POST',
        url: `https://${DOMAIN}/oauth/token`,
        headers: { 'content-type': 'application/x-www-form-urlencoded' },
        data: qs.stringify({
            grant_type: 'password',
            username: LOGIN,
            password: PASSWORD,
            audience: AUDIENCE,
            scope: 'openid profile offline_access',
            client_id: CLIENT_ID,
            client_secret: CLIENT_SECRET
        })
    };

    try {
        const response = await axios(options);
        return response.data;
    } catch (error) {
        throw new Error('Invalid credentials or request failed');
    }
};

module.exports = { getUserTokens };