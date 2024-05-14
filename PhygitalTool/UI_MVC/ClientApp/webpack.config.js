const path = require('path');

module.exports = {
    entry: {
        site: './src/ts/site.ts'
    },
    output: {
        filename: 'site.entry.js',
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
                test: /\.s?css$/,
                use: ['style-loader', 'css-loader', 'sass-loader']
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
    }
};