// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.GameObjectRepresentation;
using Improbable.Worker.Core;

namespace Generated.Improbable.Gdk.Tests.ComponentsWithNoFields
{
    public partial class ComponentWithNoFieldsWithCommands
    {
        internal class GameObjectComponentDispatcher : GameObjectComponentDispatcherBase
        {
            public override ComponentType[] ComponentAddedComponentTypes => new ComponentType[]
            {
                ComponentType.ReadOnly<ComponentAdded<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>(), ComponentType.ReadOnly<GameObjectReference>()
            };

            public override ComponentType[] ComponentRemovedComponentTypes => new ComponentType[]
            {
                ComponentType.ReadOnly<ComponentRemoved<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>(), ComponentType.ReadOnly<GameObjectReference>()
            };

            public override ComponentType[] AuthorityGainedComponentTypes => new ComponentType[]
            {
                ComponentType.ReadOnly<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>(), ComponentType.ReadOnly<GameObjectReference>(),
                ComponentType.ReadOnly<Authoritative<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>()
            };

            public override ComponentType[] AuthorityLostComponentTypes => new ComponentType[]
            {
                ComponentType.ReadOnly<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>(), ComponentType.ReadOnly<GameObjectReference>(),
                ComponentType.ReadOnly<NotAuthoritative<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>()
            };

            public override ComponentType[] AuthorityLossImminentComponentTypes => new ComponentType[]
            {
                ComponentType.ReadOnly<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>(), ComponentType.ReadOnly<GameObjectReference>(),
                ComponentType.ReadOnly<AuthorityLossImminent<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>()
            };

            public override ComponentType[] ComponentsUpdatedComponentTypes => new ComponentType[]
            {
            };

            public override ComponentType[][] EventsReceivedComponentTypeArrays => new ComponentType[][]
            {
            };

            public override ComponentType[][] CommandRequestsComponentTypeArrays => new ComponentType[][]
            {
                new ComponentType[] { ComponentType.ReadOnly<CommandRequests.Cmd>(), ComponentType.ReadOnly<GameObjectReference>() },
            };

            public override ComponentType[][] CommandResponsesComponentTypeArrays => new ComponentType[][]
            {
                new ComponentType[] { ComponentType.ReadOnly<CommandResponses.Cmd>(), ComponentType.ReadOnly<GameObjectReference>() },
            };

            private const uint componentId = 1005;
            private static readonly InjectableId readerWriterInjectableId = new InjectableId(InjectableType.ReaderWriter, componentId);
            private static readonly InjectableId commandRequestHandlerInjectableId = new InjectableId(InjectableType.CommandRequestHandler, componentId);
            private static readonly InjectableId commandResponseHandlerInjectableId = new InjectableId(InjectableType.CommandResponseHandler, componentId);

            public override void MarkComponentsAddedForActivation(Dictionary<Unity.Entities.Entity, MonoBehaviourActivationManager> entityToManagers)
            {
                if (ComponentAddedComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var entities = ComponentAddedComponentGroup.GetEntityArray();
                for (var i = 0; i < entities.Length; i++)
                {
                    var activationManager = entityToManagers[entities[i]];
                    activationManager.AddComponent(componentId);
                }
            }

            public override void MarkComponentsRemovedForDeactivation(Dictionary<Unity.Entities.Entity, MonoBehaviourActivationManager> entityToManagers)
            {
                if (ComponentRemovedComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var entities = ComponentRemovedComponentGroup.GetEntityArray();
                for (var i = 0; i < entities.Length; i++)
                {
                    var activationManager = entityToManagers[entities[i]];
                    activationManager.RemoveComponent(componentId);
                }
            }

            public override void MarkAuthorityGainedForActivation(Dictionary<Unity.Entities.Entity, MonoBehaviourActivationManager> entityToManagers)
            {
                if (AuthorityGainedComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var authoritiesChangedTags = AuthorityGainedComponentGroup.GetComponentDataArray<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>();
                var entities = AuthorityGainedComponentGroup.GetEntityArray();
                for (var i = 0; i < entities.Length; i++)
                {
                    var activationManager = entityToManagers[entities[i]];
                    // Call once except if flip-flopped back to starting state
                    if (IsFirstAuthChange(Authority.Authoritative, authoritiesChangedTags[i]))
                    {
                        activationManager.ChangeAuthority(componentId, Authority.Authoritative);
                    }
                }
            }

            public override void MarkAuthorityLostForDeactivation(Dictionary<Unity.Entities.Entity, MonoBehaviourActivationManager> entityToManagers)
            {
                if (AuthorityLostComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var authoritiesChangedTags = AuthorityLostComponentGroup.GetComponentDataArray<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>();
                var entities = AuthorityLostComponentGroup.GetEntityArray();
                for (var i = 0; i < entities.Length; i++)
                {
                    var activationManager = entityToManagers[entities[i]];
                    // Call once except if flip-flopped back to starting state
                    if (IsFirstAuthChange(Authority.NotAuthoritative, authoritiesChangedTags[i]))
                    {
                        activationManager.ChangeAuthority(componentId, Authority.NotAuthoritative);
                    }
                }
            }

            public override void InvokeOnComponentUpdateCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {
            }

            public override void InvokeOnEventCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {
            }

            public override void InvokeOnCommandRequestCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {
                if (!CommandRequestsComponentGroups[0].IsEmptyIgnoreFilter)
                {
                    var entities = CommandRequestsComponentGroups[0].GetEntityArray();
                    var commandRequestLists = CommandRequestsComponentGroups[0].GetComponentDataArray<CommandRequests.Cmd>();
                    for (var i = 0; i < entities.Length; i++)
                    {
                        var injectableStore = entityToInjectableStore[entities[i]];
                        if (!injectableStore.TryGetInjectablesForComponent(commandRequestHandlerInjectableId, out var commandRequestHandlers))
                        {
                            continue;
                        }
                         var commandRequestList = commandRequestLists[i];
                         foreach (Requirable.CommandRequestHandler commandRequestHandler in commandRequestHandlers)
                        {
                            foreach (var commandRequest in commandRequestList.Requests)
                            {
                                commandRequestHandler.OnCmdRequestInternal(commandRequest);
                            }
                        }
                    }
                }

            }

            public override void InvokeOnCommandResponseCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {

                if (!CommandResponsesComponentGroups[0].IsEmptyIgnoreFilter)
                {
                    var entities = CommandResponsesComponentGroups[0].GetEntityArray();
                    var commandResponseLists = CommandResponsesComponentGroups[0].GetComponentDataArray<CommandResponses.Cmd>();
                    for (var i = 0; i < entities.Length; i++)
                    {
                        var injectableStore = entityToInjectableStore[entities[i]];
                        if (!injectableStore.TryGetInjectablesForComponent(commandResponseHandlerInjectableId, out var commandResponseHandlers))
                        {
                            continue;
                        }

                        var commandResponseList = commandResponseLists[i];
                        foreach (Requirable.CommandResponseHandler commandResponseHandler in commandResponseHandlers)
                        {
                            foreach (var commandResponse in commandResponseList.Responses)
                            {
                                commandResponseHandler.OnCmdResponseInternal(commandResponse);
                            }
                        }
                    }
                }
            }

            public override void InvokeOnAuthorityGainedCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {
                if (AuthorityGainedComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var entities = AuthorityGainedComponentGroup.GetEntityArray();
                var changeOpsLists = AuthorityGainedComponentGroup.GetComponentDataArray<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>();

                // Call once on all entities unless they flip-flopped back into the state they started in
                for (var i = 0; i < entities.Length; i++)
                {
                    var injectableStore = entityToInjectableStore[entities[i]];
                    if (!injectableStore.TryGetInjectablesForComponent(readerWriterInjectableId, out var readersWriters))
                    {
                        continue;
                    }

                    if (IsFirstAuthChange(Authority.Authoritative, changeOpsLists[i]))
                    {
                        foreach (Requirable.ReaderWriterImpl readerWriter in readersWriters)
                        {
                            readerWriter.OnAuthorityChange(Authority.Authoritative);
                        }
                    }
                }
            }

            public override void InvokeOnAuthorityLostCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {
                if (AuthorityLostComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var entities = AuthorityLostComponentGroup.GetEntityArray();
                var changeOpsLists = AuthorityLostComponentGroup.GetComponentDataArray<AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component>>();

                // Call once on all entities unless they flip-flopped back into the state they started in
                for (var i = 0; i < entities.Length; i++)
                {
                    var injectableStore = entityToInjectableStore[entities[i]];
                    if (!injectableStore.TryGetInjectablesForComponent(readerWriterInjectableId, out var readersWriters))
                    {
                        continue;
                    }

                    if (IsFirstAuthChange(Authority.NotAuthoritative, changeOpsLists[i]))
                    {
                        foreach (Requirable.ReaderWriterImpl readerWriter in readersWriters)
                        {
                            readerWriter.OnAuthorityChange(Authority.NotAuthoritative);
                        }
                    }
                }
            }

            private bool IsFirstAuthChange(Authority authToMatch, AuthorityChanges<Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFieldsWithCommands.Component> changeOps)
            {
                foreach (var auth in changeOps.Changes)
                {
                    if (auth != Authority.AuthorityLossImminent) // not relevant
                    {
                        return auth == authToMatch;
                    }
                }
                return false;
            }

            public override void InvokeOnAuthorityLossImminentCallbacks(Dictionary<Unity.Entities.Entity, InjectableStore> entityToInjectableStore)
            {
                if (AuthorityLossImminentComponentGroup.IsEmptyIgnoreFilter)
                {
                    return;
                }

                var entities = AuthorityLossImminentComponentGroup.GetEntityArray();

                // Call once on all entities
                for (var i = 0; i < entities.Length; i++)
                {
                    var injectableStore = entityToInjectableStore[entities[i]];
                    if (!injectableStore.TryGetInjectablesForComponent(readerWriterInjectableId, out var readersWriters))
                    {
                        continue;
                    }
                    foreach (Requirable.ReaderWriterImpl readerWriter in readersWriters)
                    {
                        readerWriter.OnAuthorityChange(Authority.AuthorityLossImminent);
                    }
                }
            }
        }
    }
}