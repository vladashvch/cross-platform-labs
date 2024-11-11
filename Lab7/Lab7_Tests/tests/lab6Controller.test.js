const https = require('https');
const logger = require('../config/logger');
const { getUserTokens } = require('./test_helper');

const PORT = process.env.PORT || 7125;
const BASE_PATH = '/lab6';

describe('Lab6 API Tests', () => {
    let access_token;

    beforeAll(async () => {
        logger.info('Starting Lab6 API tests');
        const tokens = await getUserTokens();
        access_token = tokens.access_token;
    });

    afterAll(() => {
        logger.info('Finished Lab6 API tests');
    });

    test('GET /medication-types/{id} should return medication type by ID', (done) => {       
    logger.info('Testing GET /medication-types/{id} endpoint');
    
    const medicationTypeId = 'OTC'; 
    
    const options = {
        hostname: 'localhost',
        port: PORT,
        path: `/lab6/medication-types/${medicationTypeId}`, 
        method: 'GET',
        headers: {
            Authorization: `Bearer ${access_token}`,
        },
        agent: new https.Agent({
            rejectUnauthorized: false,
        }),
        timeout: 10000,
    };

    const req = https.request(options, (res) => {
        let data = '';

        res.on('data', (chunk) => {
            data += chunk;
        });

        res.on('end', () => {
            logger.info(`Received medication type by ID: ${data}`);

            expect(res.statusCode).toBe(200);

            try {
                const parsedData = JSON.parse(data);
                expect(parsedData).toHaveProperty('MedicationTypeCode'); 
            } catch (error) {
                logger.error('Error parsing response data');
            }

            done();
        });
    });

        req.on('error', (error) => {
            logger.error(`Error testing GET /medication-types/{id}: ${error.message}`);
            done(error); 
        });

        req.end();
    });

    test('GET /staff should return all staff', (done) => {
        logger.info('Testing GET /staff endpoint');

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/staff`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new https.Agent({
                rejectUnauthorized: false,
            }),
        };

        const req = https.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                logger.info(`Received staff data: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(Array.isArray(parsedData)).toBe(true);
                } catch (error) {
                    logger.error('Error parsing response data');
                }

                done();
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /staff: ${error.message}`);
            done(error); 
        });

        req.end();
    });

    test('GET /appointments should return appointments within date range', (done) => {
        const fromDate = '2024-01-01';
        const toDate = '2024-12-31';
        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/appointments?fromDate=${fromDate}&toDate=${toDate}`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new https.Agent({
                rejectUnauthorized: false,
            }),
        };

        const req = https.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                logger.info(`Received appointments: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(Array.isArray(parsedData)).toBe(true);
                } catch (error) {
                    logger.error('Error parsing response data');
                }

                done();
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /appointments: ${error.message}`);
            done(error); 
        });

        req.end();
    });
});
