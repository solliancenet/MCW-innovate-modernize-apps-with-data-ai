import React, {useState} from "react";
import {Alert, Button, Col, Form, FormGroup, Row} from "reactstrap";
import {FormField} from "./FormField";
import {IMetadataFactory} from "../interfaces/IMetadataFactory";
import {MetadataEntityType} from "../enums/MetadataEntityType";
import {WriteService} from "../services/WriteService";
import {DateFormField} from "./DateFormField";
import {v4 as uuid4} from "uuid";

export const AddFactory: React.FC<{addFactoryCallback: (f: IMetadataFactory) => void}> = ({addFactoryCallback}) => {
	const [isOpen, setIsOpen] = useState<boolean>(false);
	const [errorMessageVisible, setErrorMessageVisible] = useState(false);
	
	const [name, setName] = useState<string>("");
	
	const [latitude, setLatitude] = useState<number>(0);
	const [longitude, setLongitude] = useState<number>(0);
	const [address1, setAddress1] = useState<string>("");
	const [address2, setAddress2] = useState<string>("");
	const [city, setCity] = useState<string>("");
	const [state, setState] = useState<string>("");
	const [country, setCountry] = useState<string>("");
	const [postalCode, setPostalCode] = useState<string>("");
	const [dateInService, setDateInService] = useState<Date | null>(new Date());
	
	const resetForm = () => {
		setLatitude(0);
		setLongitude(0);
		setAddress1("");
		setAddress2("");
		setCity("");
		setState("");
		setCountry("");
		setPostalCode("");
		setDateInService(new Date());
	}
	
	const handleSubmit = (event?: React.FormEvent<HTMLFormElement>): void  => {
		setErrorMessageVisible(false);
		
		const formValues: IMetadataFactory = {
			id: uuid4(),
			name,
			entityType: MetadataEntityType.Factory,
			location: {
				latitude,
				longitude,
				address1,
				address2,
				city,
				state,
				country,
				postalCode,
			},
			dateInService: dateInService?.toUTCString() ?? "",
		};
		
		console.log("Form submitted.", formValues);
		
		WriteService.createMetadataFactoryAsync(formValues)
			.then(() => {
				setIsOpen(false);
				addFactoryCallback(formValues);
				resetForm();
			})
			.catch(e => {
				setErrorMessageVisible(true);
				console.error(e);
			});
		
		event?.preventDefault();
	}
	
	return (
		<div>
			{isOpen
				? <Form onSubmit={handleSubmit}>
					<FormField name="Name" value={name} setValue={setName} />
					<FormField name="Latitude" value={latitude} setValue={setLatitude} />
					<FormField name="Longitude" value={longitude} setValue={setLongitude} />
					<FormField name="Address 1" value={address1} setValue={setAddress1} />
					<FormField name="Address 2" value={address2} setValue={setAddress2} />
					<FormField name="City" value={city} setValue={setCity} />
					<FormField name="State" value={state} setValue={setState} />
					<FormField name="Country" value={country} setValue={setCountry} />
					<FormField name="Postal Code" value={postalCode} setValue={setPostalCode} />
					<DateFormField name="Date in Service" value={dateInService} setValue={setDateInService} />
					
					{errorMessageVisible
						? <Row>
							<Col sm={{size: 6, offset: 2}}>
								<Alert color="danger">Something went wrong.</Alert>
							</Col>
						</Row>
						: null}
					
					<FormGroup row>
						<Col sm={{size: 2, offset: 2}}>
							<Button color="primary" onClick={() => handleSubmit()}>Submit</Button>
							<Button onClick={() => setIsOpen(false)} style={{marginLeft: "6px"}}>Close</Button>
						</Col>
					</FormGroup>
				</Form>
				: <Button color="primary" onClick={() => setIsOpen(!isOpen)} style={{marginBottom: "6px"}}>Add Factory</Button>}
		</div>
	);
}