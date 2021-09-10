export class MBArtist {
    public id: number;
    public static Parse(json: any) : MBArtist {
        var res: MBArtist = new MBArtist();
        res.id = json.artist_id;
        return res;
    }
}