import { FileInformation } from 'src/app/shared/models/file';

export class MusicianFileInformation extends FileInformation {
    public MusicianId: number;
    public id: number;
    public isActive: boolean;
    public isDefault: boolean;
    constructor(fileInformation: FileInformation, MusicianId: number, url: string) {
        super(fileInformation.fileType, fileInformation.subType);
        this.MusicianId = MusicianId;
        this.url = url;
    }
}
