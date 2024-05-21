const path = require('path');
const MiniCssExtractPlugin =  require("mini-css-extract-plugin");
const CopyPlugin = require("copy-webpack-plugin");
module.exports = {
    entry: {
        site: './src/ts/site.ts',
        validation: './src/ts/Common/validation.ts',
        charts: './src/ts/Statistic/charts.ts',
        statistics: './src/ts/Statistic/statistics.ts',
        data: './src/ts/Statistic/dataFileMaker.ts',
        details: './src/ts/Flow/Details/details.ts',
        linear: './src/ts/Flow/Details/linear.ts',
        circular: './src/ts/Flow/Details/circular.ts',
        flowQuestions: './src/ts/Flow/Creation/flowquestions.ts',
        flowThemeAndType: './src/ts/Flow/Creation/flowThemeAndType.ts',
        endpage: './src/ts/Endpage/endpage.ts',
        questions: './src/ts/Questions/questions.ts',
        thema: './src/ts/Thema/thema.ts',
        organisation: './src/ts/Organisation/organisation.ts',
        upload: './src/ts/Upload/upload.ts',
        
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
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[path][name].[ext]',
                            outputPath: 'images',
                            publicPath: 'images'
                        }
                    }
                ]
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
        }),
        new CopyPlugin({
            patterns: [
                { from: 'src/images', to: 'images' },
            ]
        })
    ]
};