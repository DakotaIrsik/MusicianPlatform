export function stringGenerator(){
    var string = Math.random().toString(36).substr(2, 5)
    return string;
}