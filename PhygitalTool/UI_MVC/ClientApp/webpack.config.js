const path = require('path');
const MiniCssExtractPlugin =  require("mini-css-extract-plugin");

module.exports = {
    entry: {
        site: './src/ts/site.ts',
        validation: './src/ts/Common/validation.ts',
        charts: './src/ts/Statistic/charts.ts'
    },
    output: {
        filename: '[name].entry.js',
        path: path.resolve(__dirname, '..', 'wwwroot', 'dist'),
        clean: true
    },
    devtool: 'source-map',
    mode: 'development',
    resolve: {
        extensions: ['.ts', '.js'],
        extensionAlias: {'.js': ['.ts', '.js']}
    },
    module: {
        rules: [
            {
              test: /\.ts$/i, 
                use: ['ts-loader'],  
                exclude: /node_modules/
            },
            {
                test: /\.s?css$/i,
                use: [{loader: MiniCssExtractPlugin.loader}, 'css-loader', 'sass-loader']
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif|webp)$/i,
                type: 'asset'
            },
            {
                test: /\.(eot|woff(2)?|ttf|otf|svg)$/i,
                type: 'asset'
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: '[name].css'
        })
    ]
};