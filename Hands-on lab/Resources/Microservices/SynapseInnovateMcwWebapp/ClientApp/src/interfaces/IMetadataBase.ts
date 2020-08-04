import {MetadataEntityType} from "../enums/MetadataEntityType";

export interface IMetadataBase {
	id: string | null;
	entityType: MetadataEntityType;
}