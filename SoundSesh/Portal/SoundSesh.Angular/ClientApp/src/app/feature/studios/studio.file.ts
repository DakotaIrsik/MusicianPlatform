import { FileInformation } from 'src/app/shared/models/file';

export class StudioFileInformation extends FileInformation {
    public studioId: number;
    public id: number;
    public isActive: boolean;
    public isDefault: boolean;
    constructor(fileInformation: FileInformation, studioId: number, url: string) {
        super(fileInformation.fileType, fileInformation.subType);
        this.studioId = studioId;
        this.url = url;
    }
}
