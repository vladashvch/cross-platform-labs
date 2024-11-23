const glob = require("glob");
const path = require("path");
const webpack = require("webpack");
const miniCssExtractPlugin = require("mini-css-extract-plugin");
const { VueLoaderPlugin } = require("vue-loader");
const CopyWebpackPlugin = require('copy-webpack-plugin');

const scripts = {
    // initial point, We use to load layout and base libraries
    index: "./wwwroot/src/js/index.js",
};

module.exports = {
    // load all .js files presents in the directory merging with index
    entry: glob
        .sync("./wwwroot/src/js/**/*.js")
        .reduce(function (obj, el) {
            obj[path.parse(el).name] = `./${el}`;
            return obj;
        }, scripts),
    // set output directory, I like to clean every build
    output: {
        path: path.resolve(__dirname, "./wwwroot/dist"),
        publicPath: "/dist/",
        clean: true
    },
    // set output of webpack build stats
    stats: {
        preset: "none",
        assets: true,
        errors: true,
    },
    plugins: [
        new VueLoaderPlugin(),
        // copy all images to output dir
        new CopyWebpackPlugin({
            patterns: [
                {
                    from: path.resolve(__dirname, "./wwwroot/src/img"),
                    to: path.resolve(__dirname, "./wwwroot/dist/img")
                },
            ],
        }
        ),
    ],

    module: {
        rules: [
            {
                test: /\.css$/i,  // обробл€Їмо т≥льки CSS файли
                use: [
                    miniCssExtractPlugin.loader,  // вит€гуЇмо CSS у окрем≥ файли
                    "css-loader",  // обробл€Їмо CSS
                    {
                        loader: "postcss-loader",  // дл€ автопреф≥кс≥в та ≥нших оптим≥зац≥й
                        options: {
                            postcssOptions: {
                                plugins: function () {
                                    return [require("autoprefixer")];
                                }
                            }
                        }
                    }
                ]
            },
            {
                // this will load all js files, transpile to es5
                test: /\.js$/,
                include: path.resolve(__dirname, './wwwroot/src/js'),
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-env"]
                    }
                }
            },
            {
                //case when sass files have images references, webpack will copy
                test: /\.(jpe?g|png|gif|svg)$/,
                include: path.resolve(__dirname, './wwwroot/src/img'),
                type: "asset/resource",
                generator: {
                    filename: "img/[name].[ext]"
                }
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                type: "asset/resource",
                generator: {
                    filename: "fonts/[name].[ext]"
                }
            },
            {
                test: /\.vue$/,
                loader: "vue-loader"
            }
        ]
    },

    resolve: {
        alias: {
            // use es module vue version
            vue: "vue/dist/vue.esm-bundler.js"
        }
    },
};