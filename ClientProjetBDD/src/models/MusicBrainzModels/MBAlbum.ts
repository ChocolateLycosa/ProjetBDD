export class MBAlbum {
    public id: number;
    public static Parse(json: any): MBAlbum {
        var res: MBAlbum = new MBAlbum();
        res.id = json.id;
        return res;
    }
}