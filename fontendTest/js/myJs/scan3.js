var fs = require('fs'); /* 引用檔案物件 */
var text = fs.readFileSync(process.argv[2], "utf8"); /* 讀取檔案 */
re = /(\/\*[\s\S]*?\*\/)|(\/\/[^\r\n])|(".*?")|(\d+(\.\d*)?)|([a-zA-Z]\w*)|(\r?\n)|(\s+)|(.)/gm; /* g 代表全域，m 代表多行的比對方式。*/
console.log("text.match(re)=%j", text.match(re)); /* 印出比對後得到的陣列。*/

var log = console.log;
text = "i=3; /* hello \r\n world! */\r\n add=function(a,b) { return a+b; }";

// 本來應該用 .*? 來比對 /*...*/ 註解的，但 javascript 的 . 並不包含 \n, 因此用 \s\S 代替 . 就可以了。
// 加上後置問號 *?, +? 代表非貪婪式比對 (non greedy), m 代表多行比對模式 (multiline)
re = new RegExp(/(\/\*[\s\S]*?\*\/)|(\/\/[^\r\n])|(".*?")|(\d+(\.\d*)?)|([a-zA-Z]\w*)|(\r?\n)|(\s+)|(.)/gm);
var types = ["", "blockcomment", "linecomment", "string", "int", "float", "id", "br", "space", "op"];
var tokens = [];
var m;
var lines = 1;
while ((m = re.exec(text)) !== null) {
    var token = m[0],
        type;
    for (i = 1; i <= 8; i++) {
        if (m[i] !== undefined)
            type = types[i];
    }
    tokens.push({ "token": token, "type": type, "lines": lines });
    log("token=" + token + " type=" + type + " lines=" + lines);
    lines += token.split(/\n/).length - 1;
}

var movies = _.find(this.state.moves, (movie) => {
    return _.includes(movies.Title, "star wars");
})

var items = _.map(this.state.movies, (movie) => {
    if (_.include(movie.Title, "Star Wars")) {
        return <li > { movie.Title } < /li>;
    }
});