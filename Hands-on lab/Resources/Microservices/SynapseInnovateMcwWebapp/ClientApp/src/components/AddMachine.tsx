import React, {useState} from "react";
import {IMetadataMachine} from "../interfaces/IMetadataMachine";
import {Alert, Button, Col, Form, FormGroup, Row} from "reactstrap";
import {FormField} from "./FormField";
import {DateFormField} from "./DateFormField";
import {WriteService} from "../services/WriteService";
import {v4 as uuid4} from "uuid";
import {MetadataEntityType} from "../enums/MetadataEntityType";
import {DateTimeFormField} from "./DateTimeFormField";

export const AddMachine: React.FC<{addMachineCallback: (m: IMetadataMachine) => void}> = ({addMachineCallback}) => {
	const [isOpen, setIsOpen] = useState(false);
	const [errorMessageVisible, setErrorMessageVisible] = useState(false);
	
	const [serialNumber, setSerialNumber] = useState("");
	const [dateInService, setDateInService] = useState<Date | null>(new Date());
	const [lastMaintenanceDate, setLastMaintenanceDate] = useState<Date | null>(new Date());
	
	const resetForm = () => {
		setSerialNumber("");
		setDateInService(new Date());
		setLastMaintenanceDate(new Date());
	}
	
	const handleSubmit = (event?: React.FormEvent<HTMLFormElement>): void => {
		setErrorMessageVisible(false);
		
		const formValues: IMetadataMachine = {
			id: uuid4(),
			entityType: MetadataEntityType.Machine,
			serialNumber,
			dateInService: dateInService?.toUTCString() ?? "",
			lastMaintenanceDate: lastMaintenanceDate?.toUTCString() ?? "",
		};
		
		console.log("Form submitted.", formValues);
		
		WriteService.createMetadataMachineAsync(formValues)
			.then(() => {
				setIsOpen(false);
				addMachineCallback(formValues);
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
					<FormField name="Serial Number" value={serialNumber} setValue={setSerialNumber} />
					<DateFormField name="Date In Service" value={dateInService} setValue={setDateInService} />
					<DateTimeFormField name="Last Maintenance Date" value={lastMaintenanceDate} setValue={setLastMaintenanceDate} />

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
				: <Button color="primary" onClick={() => setIsOpen(!isOpen)} style={{marginBottom: "6px"}}>Add Machine</Button>}
		</div>
	);
}