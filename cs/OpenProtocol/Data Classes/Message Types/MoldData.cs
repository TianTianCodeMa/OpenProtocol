﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace iChen.OpenProtocol
{
	public class RequestMoldDataMessage : Message
	{
		public uint ControllerId { get; }

		public RequestMoldDataMessage (uint ControllerId, int Priority = 0) : base(Priority)
		{
			this.ControllerId = (ControllerId > 0) ? ControllerId : throw new ArgumentOutOfRangeException(nameof(ControllerId));
		}

		/// <remarks>This constructor is internal and only used for deserialization.</remarks>
		[JsonConstructor]
		internal RequestMoldDataMessage (string ID, long Sequence, uint ControllerId, int Priority) : base(ID, Sequence, Priority)
		{
			this.ControllerId = (ControllerId > 0) ? ControllerId : throw new ArgumentOutOfRangeException(nameof(ControllerId));
		}

		public override IEnumerable<KeyValuePair<string, object>> GetFields ()
		{
			yield return new KeyValuePair<string, object>(nameof(ControllerId), ControllerId);
			foreach (var field in base.GetFields()) yield return field;
		}
	}

	public class MoldDataMessage : CycleDataMessage
	{
		public MoldDataMessage (uint ControllerId, string JobCardId, string MoldId, uint OperatorId, OpModes OpMode, JobModes JobMode, IReadOnlyDictionary<string, double> Data, DateTime TimeStamp = default(DateTime), int Priority = 0)
			: base(ControllerId, JobCardId, MoldId, OperatorId, OpMode, JobMode, Data, TimeStamp, Priority) { }

		/// <remarks>This constructor is internal and only used for deserialization.</remarks>
		[JsonConstructor]
		internal MoldDataMessage (string ID, long Sequence, uint ControllerId, string JobCardId, string MoldId, uint OperatorId, OpModes OpMode, JobModes JobMode, IReadOnlyDictionary<string, double> Data, DateTime TimeStamp, int Priority)
			: base(ID, Sequence, ControllerId, JobCardId, MoldId, OperatorId, OpMode, JobMode, Data, TimeStamp, Priority) { }
	}

	public class ReadMoldDataMessage : Message
	{
		public uint ControllerId { get; }
		public string Field { get; }

		public ReadMoldDataMessage (uint ControllerId, string field, int Priority = 0) : base(Priority)
		{

			this.ControllerId = (ControllerId > 0) ? ControllerId : throw new ArgumentOutOfRangeException(nameof(ControllerId));
			this.Field = !string.IsNullOrWhiteSpace(field) ? field : throw new ArgumentNullException(nameof(field));
		}

		/// <remarks>This constructor is internal and only used for deserialization.</remarks>
		[JsonConstructor]
		internal ReadMoldDataMessage (string ID, long Sequence, uint ControllerId, string field, int Priority) : base(ID, Sequence, Priority)
		{
			if (string.IsNullOrWhiteSpace(field)) throw new ArgumentNullException(nameof(field));

			this.ControllerId = (ControllerId > 0) ? ControllerId : throw new ArgumentOutOfRangeException(nameof(ControllerId));
			this.Field = !string.IsNullOrWhiteSpace(field) ? field : throw new ArgumentNullException(nameof(field));
		}

		public override IEnumerable<KeyValuePair<string, object>> GetFields ()
		{
			yield return new KeyValuePair<string, object>(nameof(ControllerId), ControllerId);
			yield return new KeyValuePair<string, object>(nameof(Field), Field);
			foreach (var field in base.GetFields()) yield return field;
		}
	}

	public class MoldDataValueMessage : ReadMoldDataMessage
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
		public double Value { get; }

		public MoldDataValueMessage (uint ControllerId, string field, double value, int Priority = 0) : base(ControllerId, field, Priority)
		{
			this.Value = value;
		}

		/// <remarks>This constructor is internal and only used for deserialization.</remarks>
		[JsonConstructor]
		internal MoldDataValueMessage (string ID, long Sequence, uint ControllerId, string field, double value, int Priority)
			: base(ID, Sequence, ControllerId, field, Priority)
		{
			this.Value = value;
		}

		public override IEnumerable<KeyValuePair<string, object>> GetFields ()
		{
			foreach (var field in base.GetFields()) yield return field;
			yield return new KeyValuePair<string, object>(nameof(Value), Value);
		}
	}
}